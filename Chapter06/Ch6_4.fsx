// Sequence comprehensions
// Range
let int64odds = seq { 1L..2L..1000L }
seq { 'A'..'Z' }

// Maps
seq { for i in 1..2..999 -> ((+) 1 i) }
seq { for i in 1..10 do for j in 1..10 -> if i = j then 1 else 0}
seq { for i in 1..10 do for j in 1..10 -> (i,j) }
seq { for i in seq {'a'..'b'} do for j in 1..2 -> (i,j) }

// Arbitrary sequence expressions
#nowarn "40"


// Finite sequence with pattern match for halting
let rec descend top = 
    seq {
        match top with 
        | _ when top < 0 -> ()
        | _ -> 
            yield top
            yield! descend (top - 1)
    }

descend 3
descend -3

// Dumb infinite word list
let rec fizzbuzz = seq { 
        yield "Fizz"
        yield "Buzz"
        yield! fizzbuzz
    }
in fizzbuzz

// Turning any sequence into infinite circular
let rec circular ss =
    seq { yield! ss; yield! circular ss }

circular (seq { yield '+'; yield '-' })
// val it : seq<char> = seq ['+'; '-'; '+'; '-'; ...]

// Seq.init
Seq.init 10 (sprintf "%s%d" "I'm element #")
//val it : seq<string> =
//  seq
//    ["I'm element # 0"; "I'm element # 1"; "I'm element # 2";
//     "I'm element # 3"; ...]

Seq.initInfinite (fun _ -> ())
|> Seq.skip (System.Int32.MaxValue)
//> 
//val it : seq<unit> =
//  Error: Enumeration based on System.Int32 exceeded System.Int32.MaxValue.

// Seq.unfold
// Oh NO! Not Fibonacci again!
let fibnums = Seq.unfold (fun (current, next) ->
                    Some(current, (next, current+next)))(1,1)

fibnums |> Seq.take 10 |> Seq.toList
// val it : int list = [1; 1; 2; 3; 5; 8; 13; 21; 34; 55]

