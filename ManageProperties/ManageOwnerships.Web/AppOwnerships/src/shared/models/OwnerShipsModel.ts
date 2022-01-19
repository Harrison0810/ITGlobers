import { OwnerShipImagesModel } from "./OwnerShipsImagesModel";

export class OwnerShipsModel {
    public ownershipId: number;
    public name: string;
    public address: string;
    public price: number;
    public codeInternal: string;
    public year: number;
    
    public fileData?: string;
    public ownershipImages: OwnerShipImagesModel[];
}