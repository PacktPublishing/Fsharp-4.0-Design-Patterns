type private DummyEnumerate<'T>() =
    interface System.Collections.Generic.IEnumerator<'T> with 
        member x.Current = Unchecked.defaultof<'T>
                  
    interface System.Collections.IEnumerator with 
        member x.Current = box Unchecked.defaultof<'T>
        member x.MoveNext() = false
        member x.Reset() = ()

    interface System.IDisposable with 
        member x.Dispose() = () 

let makeDummyEnumerator<'T>() =
    fun() -> (new DummyEnumerate<'T>()
        :> System.Collections.Generic.IEnumerator<'T>)

let makeSeq enumerator = 
    {
        new System.Collections.Generic.IEnumerable<_> with 
            member x.GetEnumerator() = enumerator()
        interface System.Collections.IEnumerable with 
            member x.GetEnumerator() =
                (enumerator() :> System.Collections.IEnumerator)
    }

let ss = makeSeq (makeDummyEnumerator<int>())

ss |> Seq.isEmpty
ss |> Seq.length
ss |> Seq.skip 10
