// Tuples
let tuple = (1,"2",fun() -> 3)

tuple = (1,"2",fun() -> 3) // does not compile because equivalence is not defined for functions

let a = 1,"car"
a = (1,"car") // true
a = (2,"jet") // false
a < (2,"jet") // true

let (elem1, elem2) = a
printfn "(%i,%s)" elem1 elem2

let (_,_,f) = tuple in
f()

type System.Tuple<'T1,'T2> with
    member t.AsString() =
        sprintf "[[%A]:[%A]]" t.Item1 t.Item2

(a |> box :?> System.Tuple<int,string>).AsString()
