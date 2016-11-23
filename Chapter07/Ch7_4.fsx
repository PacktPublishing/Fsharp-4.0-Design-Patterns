// Memoization (F# 4.0 is required)
let memoize f =
    let mutable cache = Map.empty
    fun x ->
        match cache.TryFind(x) with
        | Some res -> printfn "returned memoized";res
        | None -> let res = f x in
                  cache <- cache.Add(x,res)
                  printfn "memoized, then returned"; res

let fm = memoize (fun x -> x * x)
fm 10
fm 42
fm 10

let memoize' f =
    let cache = System.Collections.Generic.Dictionary()
    fun x ->
        match cache.TryGetValue(x) with
        | true,res -> printfn "returned memoized";res
        | _ -> let res = f x
               cache.Add(x,res)
               printfn "memoized, then returned"
               res

let disaster = memoize' (fun () -> 5)
disaster()

#nowarn "40"
let rec binomial n k = 
    if k = 0 || k = n then 1
    else
        binomial (n - 1) k + binomial (n - 1) (k - 1)

let rec memoizedBinomial =
    let memoize f =
        let cache = System.Collections.Generic.Dictionary()
        fun x ->
            match cache.TryGetValue(x) with
            | true,res -> res
            | _ -> let res = f x
                   cache.Add(x,res)
                   res
    memoize
        (fun (n,k) ->
            if k = 0 || k = n then 1
            else
                memoizedBinomial (n - 1, k) + memoizedBinomial (n - 1,k - 1))

for i in [0..10000] do ignore <| binomial 500 2
for i in [0..10000] do ignore <| memoizedBinomial (500,2)
