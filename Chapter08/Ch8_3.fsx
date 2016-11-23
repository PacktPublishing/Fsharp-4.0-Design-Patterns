/// A G G R E G A T O R S ///
// associative operation min
List.reduce min [1;2;3;4;5]
// val it : int = 1
List.reduceBack min [1;2;3;4;5]
// val it : int = 1

// non-associative operation (-)
List.reduce (-) [1;2;3;4;5]
// val it : int = -13
List.reduceBack (-) [1;2;3;4;5]
// val it : int = 3

List.sumBy (fun x -> -x) [1;2;3]
// val it : int = -6
List.minBy (fun x -> -x) [1;2;3]
// val it : int = 3

let randoms lo hi len =
    let r = System.Random()
    let max = hi + 1
    let rec generate n = seq {
        if n < len then
            yield r.Next(lo,max)
            yield! generate (n + 1)
    }
    generate 0

randoms 1 6 10000000
|> Seq.countBy id
|> Seq.toList
|> printfn "%A"