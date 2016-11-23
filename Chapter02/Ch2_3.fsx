// Define interface
type IMyInterface =
    abstract member DoIt: unit -> unit

// Implement interface...
let makeMyInterface() =
    {
        new IMyInterface with
            member __.DoIt() = printfn "Did it!"
    }

//... and use it
makeMyInterface().DoIt()

