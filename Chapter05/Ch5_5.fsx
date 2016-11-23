type PaymentInstrumentDiscount =
    | CreditCard of decimal 
    | DebitCard of decimal
    | ACH of decimal
    
    member x.ApplyDiscount payment =
        match x with
        | CreditCard d -> payment - d
        | DebitCard d -> payment - d
        | ACH d -> payment - d

printfn "Payment amount: credit card $%.2f; debit card $%.2f; ACH $%.2f"
    ((CreditCard 0.0M).ApplyDiscount 20.23M)
    ((DebitCard 0.35M).ApplyDiscount 20.23M)
    ((ACH 0.75M).ApplyDiscount 20.23M)