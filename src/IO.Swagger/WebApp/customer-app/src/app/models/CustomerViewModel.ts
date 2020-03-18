import {AddressViewModel} from "./AddressViewModel";

export class CustomerViewModel{
    customerId = '';
    name = '';
    groupid = '';
    numberoforders = '';
    address = new AddressViewModel();
}