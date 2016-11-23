let positive = function
| x when x > 0 -> true
| x when x <= 0 -> false

let positive' = function
| x when x > 0 -> true
| _ -> false

type TickTack = Tick | Tack
 
let ticker x =
    match x with
    | Tick -> printfn "Tick"
    | Tock -> printfn "Tock"
    | Tack -> printfn "Tack"

ticker Tick
ticker Tack