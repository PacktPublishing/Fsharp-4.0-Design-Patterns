open System
open System.Collections.Generic

let size = 1000

let keys = Array.zeroCreate<Guid> size
let mutable dictionary : IDictionary<Guid,int> =
    Unchecked.defaultof<IDictionary<Guid,int>>

let generate () =
    for i in 0..(size-1) do
        keys.[i] <- Guid.NewGuid()

    dictionary <- seq { for i in 0..(size-1) -> (keys.[i],i) } |> dict

generate()

let trials = 10000000
let rg = Random()

let mutable result = 0
for i in 0..trials-1 do
    result <- dictionary.Item(keys.[rg.Next(size-1)])
//////////////////////////////////////////
let keys' = Array.zeroCreate<string> size
let mutable dictionary' : IDictionary<string,int> =
    Unchecked.defaultof<IDictionary<string,int>>

let generate' () =
    for i in 0..(size-1) do
        keys'.[i] <- keys.[i].ToString("N")

    dictionary' <- seq { for i in 0..(size-1) -> (keys'.[i],i) } |> dict

generate'()

for i in 0..trials-1 do
    result <- dictionary'.Item(keys'.[rg.Next(size-1)])
