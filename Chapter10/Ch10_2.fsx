let inline constrained (param: ^a
    when ^a: equality and ^a: comparison) = ()

type Good = Good

[<NoEquality; NoComparison>]type Bad = Bad

Good |> constrained
// Compiles just fine
Bad |> constrained
// Error: type Bad does not support comparison constraint

////////////////////////////////////////////////////////////////////////////
[<AutoOpen>]
module Restrict =
    let inline private impl restricted =
        printfn "%s type is OK" (restricted.GetType().FullName) 

    type Restricting = Restrict with 
        static member ($) (Restrict, value: byte) = impl value
        static member ($) (Restrict, value: sbyte) = impl value
        static member ($) (Restrict, value: int) = impl value
        static member ($) (Restrict, value: uint32) = impl value
        static member ($) (Restrict, value: bigint) = impl value

    let inline doit restricted = Restrict $ restricted

doit 1uy
doit 1y
doit 1
doit 1u
doit 1I
doit 1L // does not compile
doit 1.0 // does not compile
doit 1.0m // does not compile
doit '1' // does not compile

//////////////////////////////////////////////////////////////////////////////////
type Bar() =
    static member doIt() = 42

type Foo< ^T when ^T: (static member doIt: unit -> int)>(data: ^T []) =
    member inline this.Invoke () = (^T : (static member doIt : unit -> int) ())

let result = (Foo([|Bar()|]).Invoke())

/////////////// Optimizations/////////////////////////////
open System
#time "on"
let x, y = DateTime.MinValue, DateTime.MaxValue
for i = 0 to 10000000 do x = y |> ignore
//Real: 00:00:00.421, CPU: 00:00:00.406, GC gen0: 115, gen1: 2, gen2: 1

open System
#time "on"
let inline eq<'a when 'a :> IEquatable<'a>> (x:'a) (y:'a) = x.Equals y
let inline (==) x y = eq x y
for i = 0 to 10000000 do x == y |> ignore
//Real: 00:00:00.022, CPU: 00:00:00.015, GC gen0: 0, gen1: 0, gen2: 0