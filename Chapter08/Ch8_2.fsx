/// G E N E R A T O R S ///
let el = List.empty<string>
// val el : string list = []
let ea = Array.empty<float>
// val ea : float [] = [||]
let es = Seq.empty<int -> string>
// val es : seq<(int -> string)>
// es;;
// val it : seq<(int -> string)> = seq []

let ell: string list = []
// val ell : string list = []
let eal: float[] = [||]
// val eal : float [] = [||]
let esl: seq<int -> string> = seq []
// val esl : seq<(int -> string)> = []
// esl;;
// val it : seq<(int -> string)> = []

let sl = List.singleton "I'm alone"
// val sl : string list = ["I'm alone"]
let sa = Array.singleton 42.0
// val sa : float [] = [|42.0|]
let ss = Seq.singleton (fun (x:int) -> x.ToString())
// val ss : seq<(int -> string)>
// ss;;
// val it : seq<(int -> string)> = seq [<fun:ss@24>]

let sll = ["I'm alone"]
// val sll : string list = ["I'm alone"]
let sal = [| 42.0 |]
// val sal : float [] = [|42.0|]
let ssl = seq [fun (x:int) -> x.ToString()]
// val ssl : seq<(int -> string)> = [<fun:ssl@24>]

let fl = List.replicate 3 "blah"
// val fl : string list = ["blah"; "blah"; "blah"]
let fa = Array.replicate 3 42
// val fa : int [] = [|42; 42; 42|]
let fs = Seq.replicate 3 42.0
// val fs : seq<float>
// fs;;
// val it : seq<float> = seq [42.0; 42.0; 42.0]

let fll = ["blah";"blah";"blah"]
// val fll : string list = ["blah"; "blah"; "blah"]
let fal = [| for i in 1..3 -> 42 |]
// val fal : int [] = [|42; 42; 42|]
let fsl = seq { for i in 1..3 do yield 42.0 }
// val fsl : seq<float>
// fsl;;
// val it : seq<float> = seq [42.0; 42.0; 42.0]

let fac = Array.create 3 "blah"
// val fac : string [] = [|"blah"; "blah"; "blah"|]

let fazc: string[] = Array.zeroCreate 3
// val fazc : string [] = [|null; null; null|]

let fazci = Array.zeroCreate<int> 3
// val fazci : int [] = [|0; 0; 0|]

let vl = List.init 4 ((*) 2)
// val vl : int list = [0; 2; 4; 6]
let va = let src = "closure" in Array.init src.Length (fun i -> src.[i])
// val va : char [] = [|'c'; 'l'; 'o'; 's'; 'u'; 'r'; 'e'|]
let vs = Seq.init 3 id 
// val vs : seq<int>
// vs;;
// val it : seq<int> = seq [0; 1; 2]

let vll = [0; 2; 4; 6]
// val vll : int list = [0; 2; 4; 6]
let vlc = [ for i in 0..3 -> i * 2 ]
// val vlc : int list = [0; 2; 4; 6]
let vlcy = [ for i in 0..3 do yield i * 2 ]
// val vlcy : int list = [0; 2; 4; 6]
let ``val`` =
    let src = "closure" in
    [| src.[0]; src.[1]; src.[2]; src.[3]; src.[4]; src.[5]; src.[6] |]
// val val : char [] = [|'c'; 'l'; 'o'; 's'; 'u'; 'r'; 'e'|]
let vac =
    let src = "closure" in
    [| for i in 1..src.Length -> src.[i - 1] |]
// val vac : char [] = [|'c'; 'l'; 'o'; 's'; 'u'; 'r'; 'e'|]
let vacy =
    let src = "closure" in
    [| for i in 1..src.Length do
       yield src.[i - 1] |> System.Char.ToUpper |]
// val vacy : char [] = [|'C'; 'L'; 'O'; 'S'; 'U'; 'R'; 'E'|]
let vsc = seq { for i in 0..2..6 -> i}
// vsc;;
// val it : seq<int> = seq [0; 2; 4; 6]
let vscy = seq { for i in 0..2..6 do yield 6 - i }
// vscy;;
// val it : seq<int> = seq [6; 4; 2; 0]

// List of random numbers generator
let randoms lo hi len =
    let r = System.Random()
    let max = hi + 1
    let rec generate n = [
        if n < len then
            yield r.Next(lo,max)
            yield! generate (n + 1)
    ]
    generate 0


// with Seq.unfold
let collatzLib n =
    Seq.unfold (fun n -> match n with
                            | 0L -> None
                            | 1L -> Some(1L, 0L)
                            | n when n % 2L = 0L -> Some(n, n/2L)
                            | n -> Some(n, 3L * n + 1L)) n

// Collatz sequence generator
// with sequence expression
let rec collatzCustom num = 
  seq {
    yield num
    match num with
    | 1L -> ()
    | x when x % 2L = 0L -> yield! collatzCustom (x/2L)                                            
    | x -> yield! collatzCustom ((x * 3L) + 1L)
  }

[2L..1000000L] |> Seq.map (collatzLib >> Seq.length) |> Seq.max
[2L..1000000L] |> Seq.map (collatzCustom >> Seq.length) |> Seq.max
