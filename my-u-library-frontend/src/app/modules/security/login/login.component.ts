import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SecurityService } from 'src/app/services/security.service';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.scss']
})
export class LoginComponent implements OnInit {
  public form!: FormGroup;
  constructor(private fb: FormBuilder, private securityService: SecurityService, private router: Router, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.form = this.fb.group({
      email: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    const formData = this.form.getRawValue();
    this.securityService.login(formData).subscribe(
      (response) => {
        this.securityService.setData(response.data);
        this.router.navigate(['/home']);
      },
      (error) => {
        console.log(error);
        if (error.errorCode) {
          if (error.errorCode == 401) {
            this.toastr.error('Authentication failed', 'Notification');
          } else {
            this.toastr.error('An error ocurred', 'Notification');
          }
        }
      }
    )
  }
}
