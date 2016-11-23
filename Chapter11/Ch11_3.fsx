#I __SOURCE_DIRECTORY__
#r "../packages/FSharp.Control.Reactive.3.4.1/lib/net45/FSharp.Control.Reactive.dll"
#r "../packages/Rx-Core.2.2.5/lib/net45/System.Reactive.Core.dll"
#r "../packages/Rx-Interfaces.2.2.5/lib/net45/System.Reactive.Interfaces.dll"
#r "../packages/Rx-Linq.2.2.5/lib/net45/System.Reactive.Linq.dll"

open System.Reactive.Subjects

type PaymentFlowEvent =
| HeartBeat
| ACHOrigination
| GuardOn

type GuardACHOrigination(flow: Subject<PaymentFlowEvent>, alerter: Subject<string>) =
    let threshold = 3
    let mutable beats = 0
    let mutable guardOn = false

    member x.Guard() =
        beats <- 0
        guardOn <- false
        flow.Subscribe(function
                             | HeartBeat -> if guardOn then beats <- beats + 1;
                                            printfn "Heartbeat processed";
                                            if beats > threshold && guardOn then alerter.OnNext "No timely ACHOrigination"
                             | ACHOrigination -> beats <- 0;
                                                 guardOn <- false
                                                 printfn "ACHOrigination processed"
                             | GuardOn -> beats <- 0; guardOn <- true; printfn "ACHOrigination is guarded")

let paymentFlow = new Subject<PaymentFlowEvent>()
let alerter = new Subject<string>()
let notifier = alerter.Subscribe(fun x -> printfn "Logged error %s" x)

ignore <| GuardACHOrigination(paymentFlow,alerter).Guard()

paymentFlow.OnNext(HeartBeat)
paymentFlow.OnNext(GuardOn)
paymentFlow.OnNext(HeartBeat)
paymentFlow.OnNext(ACHOrigination)
paymentFlow.OnNext(GuardOn)
paymentFlow.OnNext(HeartBeat)
paymentFlow.OnNext(HeartBeat)
paymentFlow.OnNext(HeartBeat)
paymentFlow.OnNext(HeartBeat)
paymentFlow.OnNext(ACHOrigination)
paymentFlow.OnNext(HeartBeat)
paymentFlow.OnNext(HeartBeat)
paymentFlow.OnNext(HeartBeat)
paymentFlow.OnNext(HeartBeat)
