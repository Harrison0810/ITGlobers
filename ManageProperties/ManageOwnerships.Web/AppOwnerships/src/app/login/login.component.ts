import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ConstantsService } from 'src/shared/services/constants.service';
import { MessageModel } from 'src/shared/models';
import { Router } from '@angular/router';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent {
  public user: string;
  public password: string;

  constructor(
    private http: HttpClient,
    private _constants: ConstantsService,
    private router: Router
  ) {
  }

  public Login(): void {
    if (this.user && this.password) {
      const ownerModel = {
        'Username': this.user,
        'Password': this.password
      }

      this.http.post(this._constants.URLS.Login(), ownerModel).subscribe((result: MessageModel<any>) => {
        if (result.status) {
          // Here save token
          localStorage.setItem('ITGlobers-Token', result.data);
          // Agregar en todas las peticiones
          this.router.navigate(['/owner-ships']);
        }
      });
    }
  }
}
