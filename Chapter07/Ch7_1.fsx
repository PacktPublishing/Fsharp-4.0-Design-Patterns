// Naive implementation of factorial function
let rec ``naive factorial`` = function
| n when n = 0I -> 1I
| _ as n -> n * ``naive factorial`` (n - 1I)

``naive factorial`` 1000I
``naive factorial`` 10000I

// Shrewd implementation of factorial function
let ``wise factorial`` n =
    let rec factorial_tail_call acc = function
    | n when n = 0I -> acc
    | _ as n -> factorial_tail_call (acc * n) (n - 1I)
    factorial_tail_call 1I n

let howLong = (string >> String.length)

howLong <| ``wise factorial`` 1000I //2568
howLong <| ``wise factorial`` 10000I //35660
howLong <| ``wise factorial`` 100000I // 456674
