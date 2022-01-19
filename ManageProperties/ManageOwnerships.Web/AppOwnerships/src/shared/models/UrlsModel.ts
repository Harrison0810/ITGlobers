export class UrlsModel {
    public Login: () => string;
    public GetAllOwnerships: () => string;
    public GetOwnership: (id: string) => string;
    public UpdateOwnership: () => string;
    public DeleteOwnership: (id: string) => string;
    public AddOwnership: () => string;
}