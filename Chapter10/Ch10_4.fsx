//
//[<DefaultAugmentation(false)>] // Uncomment to augment the type
type Outcome =
| Success
| Failure
with
    member x.IsFailure =
        match x with
        | Failure -> true
        | _ -> false
    member x.IsSuccess = not x.IsFailure

///////////////////////// Augmentation
type ITransaction = interface end

type Sale =
    | DirectSale of decimal
    | ManualSale of decimal
    interface ITransaction

type Refund =
    | Refund of decimal
    interface ITransaction

//type Transaction =
//  | Sale of Sale
//  | Refund of Refund
//
//let ll: Transaction list = [Sale (DirectSale 5.00M); Sale (ManualSale 5.00M); Refund (Refund.Refund -1.00M)]

let ll': obj list = [box (DirectSale 10.00M); box (Refund -3.99M)]

let mixer (x: ITransaction) = x

let ll'': ITransaction list = [mixer(DirectSale 10.00M); mixer(Refund -3.99M)]
let ll''': list<_> = [mixer(DirectSale 10.00M); mixer(Refund -3.99M)]

#nowarn "25"
let disassemble (x: ITransaction) =
    match x with
    | :? Sale as sale -> (function DirectSale amount -> (sprintf "%s%.2f" "Direct sale: " amount, amount)
                                   | ManualSale amount -> (sprintf "%s%.2f" "Manual sale: " amount, amount)) sale
    | :? Refund as refund -> (function Refund amount -> (sprintf "%s%.2f" "Refund: " amount, amount)) refund

[mixer(DirectSale 4.12M);mixer(Refund -0.10M);mixer(ManualSale 3.62M)]
|> List.fold (fun (details, total) transaction ->
                  let message, amount = disassemble transaction in
                      (message::details, total + amount))
             ([],0.00M)
|> fun (details,total) ->
       (sprintf "%s%.2f" "Total: " total) :: details
|> List.iter (printfn "%s")
    