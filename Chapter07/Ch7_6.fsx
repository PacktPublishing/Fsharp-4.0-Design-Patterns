// CPS
let rec ``factorial (cps)`` cont = function
    | z when z = 0I -> cont 1I
    | n -> ``factorial (cps)`` (fun x -> cont(n * x)) (n - 1I)

let howLong = (string >> String.length)

howLong <| ``factorial (cps)`` id 10000I
