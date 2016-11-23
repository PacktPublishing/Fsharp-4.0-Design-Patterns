////////// Aggregates //////////
//average : seq<^T> -> ^T (requires member (+) and member DivideByInt and member get_Zero)
Seq.average <| seq {1. ..4.}
//averageBy : ('T -> ^U) -> seq<'T> -> ^U (requires ^U with static member (+) and ^U with static member DivideByInt and ^U with static member Zero)
seq ["1";"2";"3"] |> Seq.averageBy (double >> (+) 50.)
//fold : ('State -> 'T -> 'State) -> 'State -> seq<'T> -> 'State
(seq ["X";"y";"z";"z";"y"]) |> Seq.fold (fun a x -> a + (function "z" -> 1 | _ -> 0) x) 0
//length : seq<'T> -> int
Seq.length (seq {0..2})
//sum : seq<^T> -> ^T (requires member (+) and member get_Zero)
Seq.sum <| seq {1..3}
//sumBy : ('T -> ^U) -> seq<'T> -> ^U (requires ^U with static member (+) and ^U with static member Zero)
seq ["1";"2";"3"] |> Seq.sumBy (((+) "10") >> double)
//max : seq<'T> -> 'T (requires comparison)
seq ["11";"20";"3"] |> Seq.max
//maxBy : ('T -> 'U) -> seq<'T> -> 'T (requires comparison)
seq ["11";"20";"3"] |> Seq.maxBy int
//min : seq<'T> -> 'T (requires comparison)
seq ["11";"20";"3"] |> Seq.min
//minBy : ('T -> 'U) -> seq<'T> -> 'T (requires comparison)
seq ["11";"20";"3"] |> Seq.minBy float
//isEmpty : seq<'T> -> bool
Seq.isEmpty Seq.empty
//reduce : ('T -> 'T -> 'T) -> seq<'T> -> 'T
(seq ["X";"y";"z";"z";"y"]) |> Seq.reduce (fun a x -> a + x)
//exactlyOne : seq<'T> -> 'T
Seq.exactlyOne ["42"]
Seq.exactlyOne "42" // ArgumentException
//compareWith : ('T -> 'T -> int) -> seq<'T> -> seq<'T> -> int

////////// Generators //////////
//empty : seq<'T>
Seq.empty
//init : int -> (int -> 'T) -> seq<'T>
(*) 100 |> Seq.init 5 
//initInfinite : (int -> 'T) -> seq<'T>
Seq.initInfinite ((*) 100 >> string)
//singleton : 'T -> seq<'T>
Seq.singleton ['*';'.']
//unfold : ('State -> 'T * 'State option) -> 'State -> seq<'T>
Seq.unfold (fun current -> Some(fst current + snd current, (snd current, fst current + snd current))) (1,1)

////////// Wrappers and Type Converters //////////
//cast : IEnumerable -> seq<'T>
let s = System.Collections.Stack() in s.Push(1);s.Push('2');s.Push("xyzzy");s |> Seq.cast<int> |> printfn "%A" //!!!
//cache : seq<'T> -> seq<'T>
let cached = Seq.cache <| Seq.init 10 (fun _ -> System.DateTime.Now.ToString("yyyyMMddHHmmss.fffffff"))
cached |> Seq.iter (printfn "%A")
//delay : (unit -> seq<'T>) -> seq<'T>
let makeSequence () =  (fun () -> let ts = System.DateTime.Now.ToString("yyyyMMddHHmmss.fffffff") in printfn "making %A" ts; seq[ts])()
let makeSequence () =  (fun () -> let ts = System.DateTime.Now.ToString("yyyyMMddHHmmss.fffffff") in printfn "making %A" ts; seq[ts]) |> Seq.delay
let s1 = makeSequence ()
let s2 = makeSequence ()
printfn "Making %A" s1 
printfn "Making %A" s2
//readonly : seq<'T> -> seq<'T>
//!!!!!!!!!!!!!!!!!???????????????????
//toArray : seq<'T> -> 'T []
Seq.toArray <| seq {1..10}
//toList : seq<'T> -> 'T list
Seq.toList <| seq {1..10}
//ofArray : 'T array -> seq<'T>
[|1..10|] |> Seq.ofArray
//ofList : 'T list -> seq<'T>
[1..10] |> Seq.ofList

////////// Appliers //////////
let seqN = [1;2;3]
let seqS = ["A";"B";"C"]
//iter : ('T -> unit) -> seq<'T> -> unit
Seq.iter (printfn "%A") seqN
//iter2 : ('T1 -> 'T2 -> unit) -> seq<'T1> -> seq<'T2> -> unit
Seq.iter2 (printfn "%s=%d") seqS seqN
//iteri : (int -> 'T -> unit) -> seq<'T> -> unit
Seq.iteri (printfn "Element.[%i]=%s") seqS

////////// Recombinators //////////
//append : seq<'T> -> seq<'T> -> seq<'T>
//collect : ('T -> 'Collection) -> seq<'T> -> seq<'U>
//concat : seq<'Collection> -> seq<'T>
//head : seq<'T> -> 'T
//last : seq<'T> -> 'T
//nth : int -> seq<'T> -> 'T
//skip : int -> seq<'T> -> seq<'T>
//take : int -> seq<'T> -> seq<'T>
//sort : seq<'T> -> seq<'T>
//sortBy : ('T -> 'Key) -> seq<'T> -> seq<'T>
//truncate : int -> seq<'T> -> seq<'T>
//distinct : seq<'T> -> seq<'T>
//distinctBy : ('T -> 'Key) -> seq<'T> -> seq<'T>

////////// Filters //////////
//choose : ('T -> 'U option) -> seq<'T> -> seq<'U>
//exists : ('T -> bool) -> seq<'T> -> bool
//exists2 : ('T1 -> 'T2 -> bool) -> seq<'T1> -> seq<'T2> -> bool
//filter : ('T -> bool) -> seq<'T> -> seq<'T>
//find : ('T -> bool) -> seq<'T> -> 'T
//findIndex : ('T -> bool) -> seq<'T> -> int
//forall : ('T -> bool) -> seq<'T> -> bool
//forall2 : ('T1 -> 'T2 -> bool) -> seq<'T1> -> seq<'T2> -> bool
//pick : ('T -> 'U option) -> seq<'T> -> 'U
//skipWhile : ('T -> bool) -> seq<'T> -> seq<'T>
//takeWhile : ('T -> bool) -> seq<'T> -> seq<'T>
//tryFind : ('T -> bool) -> seq<'T> -> 'T option
//tryFindIndex : ('T -> bool) -> seq<'T> -> int option
//tryPick : ('T -> 'U option) -> seq<'T> -> 'U option
//where : ('T -> bool) -> seq<'T> -> seq<'T>

////////// Mappers //////////
//countBy : ('T -> 'Key) -> seq<'T> -> seq<'Key * int>
//groupBy : ('T -> 'Key) -> seq<'T> -> seq<'Key * seq<'T>>
//pairwise : seq<'T> -> seq<'T * 'T>
//map : ('T -> 'U) -> seq<'T> -> seq<'U>
//map2 : ('T1 -> 'T2 -> 'U) -> seq<'T1> -> seq<'T2> -> seq<'U>
//mapi : (int -> 'T -> 'U) -> seq<'T> -> seq<'U>
//scan : ('State -> 'T -> 'State) -> 'State -> seq<'T> -> seq<'State>
//windowed : int -> seq<'T> -> seq<'T []>
//zip : seq<'T1> -> seq<'T2> -> seq<'T1 * 'T2>
//zip3 : seq<'T1> -> seq<'T2> -> seq<'T3> -> seq<'T1 * 'T2 * 'T3>



