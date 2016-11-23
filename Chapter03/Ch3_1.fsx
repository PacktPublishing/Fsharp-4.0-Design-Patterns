let circleArea radius =
    System.Math.PI * radius * radius

circleArea 10.

//---------------------------------------------------------------------------
let opaque arg =
    System.DateTime.Now.Second * (if arg % 2 = 0 then 2 else 1)


//---------------------------------------------------------------------------
// int64 value
let getNextRandom = (%) System.DateTime.Now.Ticks 1000L

getNextRandom

// unit->x64 function
let getNextRandomA() = (%) System.DateTime.Now.Ticks 1000L

getNextRandomA()
getNextRandomA()

// unit->unit function
let getNextRandomB () = printfn "%d" ((%) System.DateTime.Now.Ticks 1000L)

getNextRandomB()
