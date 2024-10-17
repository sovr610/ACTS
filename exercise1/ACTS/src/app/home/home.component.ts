import { Component, OnInit, AfterViewInit, ViewChild, TemplateRef  } from '@angular/core';
import { AstronautService } from '../astronaut.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

interface Person {
  personId: number;
  name: string;
  currentRank?: string;
  currentDutyTitle?: string;
  careerStartDate?: string;
  careerEndDate?: string;
}

interface AstronautDuty {
  rank: string;
  dutyTitle: string;
  dutyStartDate: string;
  dutyEndDate: string | null;
}

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit, AfterViewInit {
  @ViewChild('personDetailsModal') personDetailsModal!: TemplateRef<any>;

  people: Person[] = [];
  filteredPeople: Person[] = [];
  selectedPerson: Person | null = null;
  selectedPersonDuties: AstronautDuty[] = [];
  errorMessage: string = '';
  searchTerm: string = '';
  isSearching: boolean = false;

  constructor(
    private astronautService: AstronautService,
    private modalService: NgbModal
  ) {}

  ngOnInit(): void {
    this.loadPeople();
  }

  ngAfterViewInit(): void {

  }

  loadPeople(): void {
    this.astronautService.getPeople().subscribe(
      (data: Person[]) => {
        console.log('People data:', data);
        this.people = data;
        this.filteredPeople = data;
        this.errorMessage = '';
      },
      (error: any) => {
        console.error('Error fetching people', error);
        this.errorMessage = 'Failed to load people. Please try again later.';
      }
    );
  }



  loadAstronautDuties(name: string): void {
    this.astronautService.getAstronautDutiesByName(name).subscribe(
      (data: AstronautDuty[]) => {
        console.log('Astronaut duties:', data);
        this.selectedPersonDuties = data;
        this.errorMessage = '';
      },
      (error: any) => {
        console.error('Error fetching astronaut duties', error);
        this.selectedPersonDuties = [];
        this.errorMessage = `Failed to load duties for ${name}. ${error.message || 'Please try again later.'}`;
      }
    );
  }

  isAstronaut(person: Person): boolean {
    return !!person.currentDutyTitle;
  }

  calculateProgress(duty: AstronautDuty): number {
    const start = new Date(duty.dutyStartDate);
    const end = duty.dutyEndDate ? new Date(duty.dutyEndDate) : new Date();
    const total = end.getTime() - start.getTime();
    const elapsed = new Date().getTime() - start.getTime();
    return Math.min((elapsed / total) * 100, 100);
  }

  getProgressBarColor(progress: number): string {
    if (progress < 33) return '#dc3545'; // Bootstrap danger color
    if (progress < 66) return '#ffc107'; // Bootstrap warning color
    return '#28a745'; // Bootstrap success color
  }

  formatDateTime(dateTimeString: string | null | undefined): string {
    if (!dateTimeString) return 'N/A';
    const date = new Date(dateTimeString);
    return date.toLocaleDateString();
  }

  getCareerEndDate(person: Person | null): string {
    if (person && person.careerEndDate) {
      return this.formatDateTime(person.careerEndDate);
    }
    return 'Ongoing';
  }

  selectPerson(person: Person): void {
    this.selectedPerson = person;
    this.loadAstronautDuties(person.name);
    this.openPersonDetailsModal();
  }
  searchPeople(): void {
    this.isSearching = this.searchTerm.trim().length > 0;
    if (this.isSearching) {
      this.filteredPeople = this.people.filter(person =>
        person.name.toLowerCase().includes(this.searchTerm.toLowerCase())
      );
    } else {
      this.filteredPeople = this.people;
    }
  }

  openPersonDetailsModal(): void {
    if (this.selectedPerson) {
      this.modalService.open(this.personDetailsModal, { size: 'lg' });
    }
  }
}
