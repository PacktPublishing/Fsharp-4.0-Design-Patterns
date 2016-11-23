// Template design pattern
open System

type PayBy = ACH | Check | Wire
             override x.ToString() =
                match x with
                | ACH -> "By ACH"
                | Check -> "By Check"
                | Wire -> "By Wire"

type Payment = string
type BankReqs = { ABA: string; Account: string}
type Merchant = { MerchantId: Guid; Requisites: BankReqs }

type ITemplate =
    abstract GetPaymentDue: Guid -> Merchant*decimal
    abstract FormatPayment: Merchant*decimal -> Payment
    abstract SubmitPayment: Payment -> bool

let Template  payBy =
    { new ITemplate with
        member __.GetPaymentDue merchantId =
            printfn "Getting payment due of %s" (merchantId.ToString())
            (* mock access to ERP getting Accounts payable due for merchantId *)
            ({ MerchantId = merchantId; Requisites = {ABA="021000021"; Account="123456789009"} }, 25366.76M)
        member __.FormatPayment (m,t)  =
            printfn "Formatting payment of %s" (m.MerchantId.ToString())
            sprintf "%s:%s:%s:%s:%.2f" "Payment to" m.Requisites.ABA m.Requisites.Account (payBy.ToString()) t
        member __.SubmitPayment p =
            printfn "Submitting %s..." p
            true
        }

let makePayment merchantId payBy  =
    let template = Template payBy in
    template.GetPaymentDue merchantId
    |> template.FormatPayment
    |> template.SubmitPayment

makePayment (Guid.NewGuid()) Check
makePayment (Guid.NewGuid()) ACH   