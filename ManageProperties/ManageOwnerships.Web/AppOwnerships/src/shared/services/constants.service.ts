import { Inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { OwnerModel, UrlsModel } from '../models'

@Injectable()
export class ConstantsService {
    private BASE_URL: string = '';
    private API_VERSION: string = ''

    public URLS: UrlsModel = {
        Login: (): string => `${this.BASE_URL}api/${this.API_VERSION}/User/Authenticate`,
        GetAllOwnerships: (): string => `${this.BASE_URL}api/${this.API_VERSION}/Ownership/GetAllOwnerships`,
        GetOwnership: (id: string): string => `${this.BASE_URL}api/${this.API_VERSION}/Ownership/GetOwnership/${id}`,
        UpdateOwnership: (): string => `${this.BASE_URL}api/${this.API_VERSION}/Ownership/UpdateOwnership`,
        DeleteOwnership: (id: string): string => `${this.BASE_URL}api/${this.API_VERSION}/Ownership/DeleteOwnership/${id}`,
        AddOwnership: (): string => `${this.BASE_URL}api/${this.API_VERSION}/Ownership/AddOwnership`,
    }

    constructor(@Inject('BASE_URL') baseUrl: string, @Inject('API_VERSION') apiVersion: string) {
        this.BASE_URL = baseUrl;
        this.API_VERSION = apiVersion;
    }
}