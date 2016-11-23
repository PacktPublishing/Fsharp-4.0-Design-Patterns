type private Repeater<'T>(repeated) =
    let _repeated = repeated 
    interface System.Collections.Generic.IEnumerator<'T> with 
        member x.Current = _repeated
                  
    interface System.Collections.IEnumerator with 
        member x.Current = box _repeated
        member x.MoveNext() = true
        member x.Reset() = ()

    interface System.IDisposable with 
        member x.Dispose() = () 

let repeat<'T>(i) =
    (new Repeater<'T>(i)
         :> System.Collections.Generic.IEnumerator<'T>)

let makeSeq enumerator = 
    {
        new System.Collections.Generic.IEnumerable<'U> with 
            member x.GetEnumerator() = enumerator
        interface System.Collections.IEnumerable with 
            member x.GetEnumerator() =
                (enumerator :> System.Collections.IEnumerator)
    }

makeSeq (repeat '.')
repeat 42 |> makeSeq
makeSeq <| repeat "Hooray!"

let inline traverse n s =
    let counter =
        (Seq.zip
        (seq { LanguagePrimitives.GenericOne..n }) s)
            .GetEnumerator()
    let i = ref LanguagePrimitives.GenericOne
    let mutable last = Unchecked.defaultof<_>
    while counter.MoveNext() do
        if !i = n then last <- counter.Current
        i := !i + LanguagePrimitives.GenericOne
    last

makeSeq <| repeat '.' |> traverse 10000I
makeSeq <| repeat 42 |> traverse ((System.Int32.MaxValue |> int64) + 10L)


