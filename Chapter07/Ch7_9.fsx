open System

let (|Recent|Due|) (dt: DateTimeOffset) =
    if DateTimeOffset.Now.AddDays(-3.0) <= dt then Recent
    else Due
    
    
let isDue = function
| Recent -> printfn "don't do anything"
| Due -> printfn "time to pay this one"

isDue <| DateTimeOffset.Now.AddDays(-2.0)
isDue <| DateTimeOffset.Now.AddDays(-4.0)

