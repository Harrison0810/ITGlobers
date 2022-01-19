import { Component, OnInit } from '@angular/core';
import { MessageModel, OwnerShipsModel } from 'src/shared/models';
import { HttpClient } from '@angular/common/http';
import { ConstantsService } from 'src/shared/services/constants.service';
import { Observable, ReplaySubject } from 'rxjs';

@Component({
  selector: 'app-owner-ships',
  templateUrl: './owner-ships.component.html',
  styleUrls: ['./owner-ships.component.css']
})
export class OwnerShipsComponent implements OnInit {
  public ownerships: OwnerShipsModel[];
  public editOwnership: OwnerShipsModel;
  public deleteOwnership: OwnerShipsModel;
  public addOwnership: OwnerShipsModel;

  constructor(
    private http: HttpClient,
    private _constants: ConstantsService
  ) { }

  public ShowAdd(): void {
    this.addOwnership = {
      address: '',
      codeInternal: '',
      name: '',
      ownershipId: 0,
      price: 0,
      year: 0,
      ownershipImages: [{
        file: '',
        Enabled: true
      }]
    };
  }

  public UploadImage(event: any, edit: boolean): void {
    const [file] = event.files
    if (file) {
      const file = event.files[0];
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => {
        if (edit) {
          this.editOwnership.fileData = reader.result.toString();
        } else {
          this.addOwnership.fileData = reader.result.toString();
        }
      };
    }
  }

  public AddOwnership(): void {
    const newOwnership: OwnerShipsModel = {
      ownershipId: 0,
      name: this.addOwnership.name,
      address: this.addOwnership.address,
      price: this.addOwnership.price,
      codeInternal: this.addOwnership.codeInternal,
      year: this.addOwnership.year,
      ownershipImages: [{
        file: this.addOwnership.fileData,
        Enabled: true
      }]
    }

    this.http.post(this._constants.URLS.AddOwnership(), newOwnership).subscribe((result: MessageModel<OwnerShipsModel[]>) => {
      if (result.status) {
        this.ownerships = result.data;
        this.addOwnership = null;
        this.GetOwnerships();
      }
    });
  }

  public SetEditOwnership(editOwnership: OwnerShipsModel): void {
    this.editOwnership = editOwnership;
  }

  public SetDeleteOwnership(deleteOwnership: OwnerShipsModel): void {
    this.deleteOwnership = deleteOwnership;
  }

  public EditOwnership(): void {
    const editOwnership: OwnerShipsModel = {
      ownershipId: this.editOwnership.ownershipId,
      name: this.editOwnership.name,
      address: this.editOwnership.address,
      price: this.editOwnership.price,
      codeInternal: this.editOwnership.codeInternal,
      year: this.editOwnership.year,
      ownershipImages: [{
        file: this.addOwnership.fileData,
        Enabled: true
      }]
    }

    this.http.post(this._constants.URLS.UpdateOwnership(), editOwnership).subscribe((result: MessageModel<OwnerShipsModel[]>) => {
      if (result.status) {
        this.ownerships = result.data;
        this.editOwnership = null;
        this.GetOwnerships();
      }
    });
  }

  public DeleteOwnership(): void {
    this.http.post(this._constants.URLS.DeleteOwnership(this.deleteOwnership.ownershipId.toString()), null).subscribe((result: MessageModel<OwnerShipsModel[]>) => {
      if (result.status) {
        this.ownerships = result.data;
        this.deleteOwnership = null;
        this.GetOwnerships();
      }
    });
  }

  private GetOwnerships(): void {
    this.http.get(this._constants.URLS.GetAllOwnerships()).subscribe((result: MessageModel<OwnerShipsModel[]>) => {
      if (result.status) {
        this.ownerships = result.data;
      }
    });
  }

  ngOnInit() {
    this.GetOwnerships();
  }
}
