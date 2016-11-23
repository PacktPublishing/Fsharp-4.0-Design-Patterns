type transport = { code: int; name: string }

let a = { code = 1; name = "car" }
let b = { name = "jet"; code = 2 }

let c = { b with transport.name = "plane" }

c = b

[<ReferenceEquality>]
type Transport = { code: int; name: string }
let x = {Transport.code=5; name="boat" }
let y = { x with name = "boat"}
let noteq = x = y
let eq = x = x

let  { transport.code = _; name = aName } = a
let { transport.name = aname} = a
let aName' = a.name