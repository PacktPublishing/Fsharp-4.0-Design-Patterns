type IMyInterface =
    abstract member DoIt: unit -> unit
    
type MyImpl() =
    interface IMyInterface with
        member __.DoIt() = printfn "Did it!"
        
MyImpl().DoIt() // Error: member 'DoIt' is not defined

(MyImpl() :> IMyInterface).DoIt()

// ... but
let doit (doer: IMyInterface) =
    doer.DoIt()

doit (MyImpl())

