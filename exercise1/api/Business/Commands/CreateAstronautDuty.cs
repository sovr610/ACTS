using Dapper;
using MediatR;
using MediatR.Pipeline;
using Microsoft.EntityFrameworkCore;
using StargateAPI.Business.Data;
using StargateAPI.Controllers;
using System.Net;

namespace StargateAPI.Business.Commands
{
    public class CreateAstronautDuty : IRequest<CreateAstronautDutyResult>
    {
        public required string Name { get; set; }

        public required string Rank { get; set; }

        public required string DutyTitle { get; set; }

        public DateTime DutyStartDate { get; set; }
    }

    public class CreateAstronautDutyPreProcessor : IRequestPreProcessor<CreateAstronautDuty>
    {
        private readonly StargateContext _context;

        public CreateAstronautDutyPreProcessor(StargateContext context)
        {
            _context = context;
        }

        public async Task Process(CreateAstronautDuty request, CancellationToken cancellationToken)
        {
            var person = await _context.People.AsNoTracking().FirstOrDefaultAsync(z => z.Name == request.Name);

            if (person is null) throw new BadHttpRequestException("Bad Request");

            var verifyNoPreviousDuty = await _context.AstronautDuties.FirstOrDefaultAsync(z => z.DutyTitle == request.DutyTitle && z.DutyStartDate == request.DutyStartDate);

            if (verifyNoPreviousDuty is not null) throw new BadHttpRequestException("Bad Request");

            //return Task.CompletedTask;
        }
    }

    public class CreateAstronautDutyHandler : IRequestHandler<CreateAstronautDuty, CreateAstronautDutyResult>
    {
        private readonly StargateContext _context;

        public CreateAstronautDutyHandler(StargateContext context)
        {
            _context = context;
        }
        public async Task<CreateAstronautDutyResult> Handle(CreateAstronautDuty request, CancellationToken cancellationToken)
        {
            var query = $"SELECT * FROM [Person] WHERE \'{request.Name}\' = Name";
            var person = await _context.Connection.QueryFirstOrDefaultAsync<Person>(query);

            if (person == null)
            {
                return new CreateAstronautDutyResult
                {
                    Success = false,
                    Message = "Person not found",
                    ResponseCode = (int)HttpStatusCode.NotFound
                };
            }

            query = $"SELECT * FROM [AstronautDetail] WHERE {person.Id} = PersonId";
            var astronautDetail = await _context.Connection.QueryFirstOrDefaultAsync<AstronautDetail>(query);

            if (astronautDetail == null)
            {
                astronautDetail = new AstronautDetail
                {
                    PersonId = person.Id,
                    CurrentDutyTitle = request.DutyTitle,
                    CurrentRank = request.Rank,
                    CareerStartDate = request.DutyStartDate.Date
                };
                await _context.AstronautDetails.AddAsync(astronautDetail);
            }
            else
            {
                astronautDetail.CurrentDutyTitle = request.DutyTitle;
                astronautDetail.CurrentRank = request.Rank;
                _context.AstronautDetails.Update(astronautDetail);
            }

            // Set end date for previous duty
            query = $"SELECT * FROM [AstronautDuty] WHERE {person.Id} = PersonId AND DutyEndDate IS NULL";
            var currentDuty = await _context.Connection.QueryFirstOrDefaultAsync<AstronautDuty>(query);
            if (currentDuty != null)
            {
                currentDuty.DutyEndDate = request.DutyStartDate.AddDays(-1).Date;
                _context.AstronautDuties.Update(currentDuty);
            }

            var newAstronautDuty = new AstronautDuty
            {
                PersonId = person.Id,
                Rank = request.Rank,
                DutyTitle = request.DutyTitle,
                DutyStartDate = request.DutyStartDate.Date,
                DutyEndDate = null
            };

            if (request.DutyTitle == "RETIRED")
            {
                astronautDetail.CareerEndDate = request.DutyStartDate.AddDays(-1).Date;
                newAstronautDuty.DutyEndDate = request.DutyStartDate.Date;
            }

            await _context.AstronautDuties.AddAsync(newAstronautDuty);
            await _context.SaveChangesAsync();

            return new CreateAstronautDutyResult
            {
                Id = newAstronautDuty.Id,
                Success = true,
                Message = "Astronaut duty created successfully",
                ResponseCode = (int)HttpStatusCode.Created
            };
        }
    }

    public class CreateAstronautDutyResult : BaseResponse
    {
        public int? Id { get; set; }
    }
}
