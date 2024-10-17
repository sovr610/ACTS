import { Component } from '@angular/core';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators, AbstractControl } from '@angular/forms';


@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  registerForm: FormGroup;

  constructor(
    private authService: AuthService,
    private router: Router,
    private formBuilder: FormBuilder
  ) {
    this.registerForm = this.formBuilder.group({
      firstName: ['', Validators.required],
      lastName: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
      username: ['', Validators.required],
      password: ['', [
        Validators.required,
        Validators.minLength(8),
        Validators.pattern(/^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$/)
      ]],
      confirmPassword: ['', Validators.required]
    }, { validators: this.passwordMatchValidator });
  }

  passwordMatchValidator(g: AbstractControl): { [key: string]: boolean } | null {
    const password = g.get('password');
    const confirmPassword = g.get('confirmPassword');
    if (password && confirmPassword && password.value !== confirmPassword.value) {
      return { 'mismatch': true };
    }
    return null;
  }

  onSubmit() {
    if (this.registerForm.valid) {
      const { firstName, lastName, email, username, password } = this.registerForm.value;
      this.authService.register(firstName, lastName, email, username, password).subscribe(
        (response: any) => {
          console.log('Registration successful', response);
          this.router.navigate(['/login']);
        },
        (error: any) => {
          console.error('Registration failed', error);
          // Handle registration error (show message to user)
        }
      );
    }
  }
}
