[<Literal>]
let THREE = 3

let transformA v =
    match v with
    | 1 -> "1"
    | 2 -> "2"
    | THREE -> "3"

transformA <| (1 + 2)

type Multiples =
    | Zero = 0
    | Five = 5
    
let transformB ``compare me`` =
    match ``compare me`` with
    | Multiples.Zero -> "0"
    | Multiples.Five -> "5"

Multiples.Five  |> transformB

enum<Multiples>(1) |> transformB

let transformB' m =
    match m with
    | Multiples.Zero -> Some "0"
    | Multiples.Five -> Some "5"
    | _ -> None

let transformB'' m =
    match m with
    | _ -> None    
    | Multiples.Zero -> Some "0"
    | Multiples.Five -> Some "5"
