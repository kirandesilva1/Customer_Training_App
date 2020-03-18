import {AddressViewModel} from "./AddressViewModel";
import {CustomerViewModel} from "./CustomerViewModel";
import {OrderItemViewModel} from "./OrderItemViewModel";

export class OrderViewModel{
    orderId: string;
    customer: CustomerViewModel;
    description: string;
    shipAddress: AddressViewModel;
    orderItems = new Array<OrderItemViewModel>();
    //status: statusEnum;
}