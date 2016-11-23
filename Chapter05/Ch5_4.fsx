type ChargeAttempt = 
    | Original
    | Retry of int

let cco = Original
// equivalent let cco = ChargeAttempt.Original

let ccr = Retry 4
// equivalent let ccr = ChargeAttempt.Retry(4)

type Brightness = Brightness of int
type Voltage = Voltage of int
type Bulb = { voltage: Voltage; brightness: Brightness }

let myBulb = { voltage = Voltage(110); brightness= Brightness(2500)}

let lamp1br = Brightness(2500)
lamp1br = Brightness(2500) // true
lamp1br < Brightness(2100) // false

match myBulb.brightness with
| Brightness(v) -> v
