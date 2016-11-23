let gr = ref [] // error: value restriction
let gr<'a> : 'a list ref = ref []
gr := ["a"]
let x = !gr // error: value restriction
let x: string list = !gr
printfn "%A" x
let cr = gr<string>
cr := ["a"]
let y = !cr
printfn "%A" y
////////////////////////////////////////////////////
let allEmpty  = List.forall ((=) []) // error: value restriction

let allEmpty xs = List.forall ((=) []) xs // remedy 1 
let allEmpty : int list list -> bool  = List.forall ((=) []) // remedy 2
let allEmpty = fun x -> List.forall ((=) []) x // remedy 3