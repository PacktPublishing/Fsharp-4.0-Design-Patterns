[<Measure>] type m // meters
[<Measure>] type s // seconds

let fallSpeed (height: float<m>) = 2.0 * height * 9.81<m/s^2> |> sqrt
let empireStateBuilding = 381.0<m>
fallSpeed empireStateBuilding