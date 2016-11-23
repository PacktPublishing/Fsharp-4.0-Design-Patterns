let ``folding factorial (seq)`` n =
    let factors = Seq.init (n + 1) bigint.op_Implicit |> Seq.skip 1
    use er = factors.GetEnumerator()
    let mutable acc = 1I
    while er.MoveNext() do
        acc <- acc * er.Current
    acc

let ``folding factorial (lib)`` n =
   Seq.init (n + 1) bigint.op_Implicit
   |> Seq.skip 1
   |> Seq.fold (*) 1I

let howLong = (string >> String.length)

howLong <| ``folding factorial (seq)`` 10000
howLong <| ``folding factorial (lib)`` 10000

// Excerpt from seq.fs of FSharp.Core.Collections:
[<CompiledName("Fold")>]
let fold<'T,'State> f (x:'State) (source : seq<'T>)  = 
    checkNonNull "source" source
    use e = source.GetEnumerator() 
    let f = OptimizedClosures.FSharpFunc<_,_,_>.Adapt(f)
    let mutable state = x 
    while e.MoveNext() do
        state <- f.Invoke(state, e.Current)
    state