let makeSeq f = 
    {
        new System.Collections.Generic.IEnumerable<'U> with 
            member x.GetEnumerator() = printfn "Fresh enumerator given"; f()
        interface System.Collections.IEnumerable with 
            member x.GetEnumerator() =
                (f() :> System.Collections.IEnumerator)
    }

//caching
let nums = (seq {1..100}).GetEnumerator |> makeSeq
// non-cached - double enumeration
((nums |> Seq.sum),(nums |> Seq.length))
//Fresh enumerator given
//Fresh enumerator given
//val it : int * int = (5050, 100)

let cache = nums |> Seq.cache
// cached - single enumeration
((cache |> Seq.sum),(cache |> Seq.length))
//Fresh enumerator given
//val it : int * int = (5050, 100)
// just another time - no enumerations at all
((cache |> Seq.sum),(cache |> Seq.length))
//val it : int * int = (5050, 100)


// fusion
let series = (seq {1..100}).GetEnumerator |> makeSeq
let average dd = (Seq.sum dd) / (Seq.length dd)
average series
//Fresh enumerator given
//Fresh enumerator given
//val it : int = 50

let averageFused dd =
    dd
    |> Seq.fold (fun acc x -> (fst acc + x, snd acc + 1)) (0,0)
    |> fun x -> fst x / snd x
averageFused series
//Fresh enumerator given
//val it : int = 50

