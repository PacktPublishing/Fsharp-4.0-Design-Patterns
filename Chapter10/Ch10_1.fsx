open System

let reverse ls =
    let rec rev acc = function
    | h::t -> rev (h::acc) t
    | []   -> acc
    rev [] ls

reverse [1;2;3]
// val it : int list = [3; 2; 1]
reverse ["1";"2";"3"]
// val it : string list = ["3"; "2"; "1"]
reverse [box 1.0; box 2.0M; box 3I]
//val it : obj list = [3 {IsEven = false;
//                        IsOne = false;
//                        IsPowerOfTwo = false;
//                        IsZero = false;
//                        Sign = 1;}; 2.0M; 1.0]

///////////////Demoing the problem with static constrains
let twice x = x <<< 1
twice 10L
// twice 10 - compilation error!

let inline twice' x = x <<< 1
twice' 5    // int32
twice' 5u   // uint32
twice' 5L   // int64
twice' 5UL  // uint64
twice' 5y   // sbyte
twice' 5uy  // byte
twice' 5s   // int16
twice' 5us  // uint16
twice' 5I   // biginteger
twice' 5n   // nativeint

twice' 5m
twice' 5.0
twice' "5"
twice' '5'

///////////////////////////////////////////
let floats = [|1.0..100000000.0|]

let inline sum' x y = x + y
 
let inline fold f a (xs: _ []) =
     let mutable a = a
     for i=0 to xs.Length-1 do
        a <- f a xs.[i]
     a

let sum'' (x:float) (y: float) = x + y
 
let fold' f a (xs: _ []) =
     let mutable a = a
     for i=0 to xs.Length-1 do
        a <- f a xs.[i]
     a
fold' sum'' 0.0 floats


fold sum' 0.0 floats
floats |> Array.fold (+) 0.0

