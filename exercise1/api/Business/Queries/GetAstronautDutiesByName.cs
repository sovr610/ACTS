using Dapper;
using MediatR;
using StargateAPI.Business.Data;
using StargateAPI.Business.Dtos;
using StargateAPI.Controllers;
using System.Net;

namespace StargateAPI.Business.Queries
{
    public class GetAstronautDutiesByName : IRequest<GetAstronautDutiesByNameResult>
    {
        public string Name { get; set; } = string.Empty;
    }

    public class GetAstronautDutiesByNameHandler : IRequestHandler<GetAstronautDutiesByName, GetAstronautDutiesByNameResult>
    {
        private readonly StargateContext _context;

        public GetAstronautDutiesByNameHandler(StargateContext context)
        {
            _context = context;
        }

        public async Task<GetAstronautDutiesByNameResult> Handle(GetAstronautDutiesByName request, CancellationToken cancellationToken)
        {

            var result = new GetAstronautDutiesByNameResult();

            var query = $"SELECT a.Id as PersonId, a.Name, b.CurrentRank, b.CurrentDutyTitle, b.CareerStartDate, b.CareerEndDate FROM [Person] a LEFT JOIN [AstronautDetail] b on b.PersonId = a.Id WHERE \'{request.Name}\' = a.Name";
            var person = await _context.Connection.QueryFirstOrDefaultAsync<PersonAstronaut>(query);

            if (person == null)
            {
                result.Success = false;
                result.Message = "Person not found";
                result.ResponseCode = (int)HttpStatusCode.NotFound;
                return result;
            }

            result.Person = person;

            if (person.CurrentDutyTitle != null)
            {
                query = $"SELECT * FROM [AstronautDuty] WHERE {person.PersonId} = PersonId Order By DutyStartDate Desc";
                var duties = await _context.Connection.QueryAsync<AstronautDuty>(query);
                result.AstronautDuties = duties.ToList();
            }

            result.Success = true;
            result.Message = "Data retrieved successfully";
            result.ResponseCode = (int)HttpStatusCode.OK;

            return result;

        }
    }

    public class GetAstronautDutiesByNameResult : BaseResponse
    {
        public PersonAstronaut Person { get; set; }
        public List<AstronautDuty> AstronautDuties { get; set; } = new List<AstronautDuty>();
    }
}
