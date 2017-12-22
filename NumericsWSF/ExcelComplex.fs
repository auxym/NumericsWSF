module ExcelComplex

// Excel doesn't handle complex numbers, so we return anything with a nonzero
// imaginary part as a string.

open System
open System.Numerics

type ExcelComplex = 
    | Complex of string
    | Real of float

let ofComplex (z:Complex) =
    let toString (v:Complex) = 
        let sg = if v.Imaginary < 0.0 then "-" else "+"
        sprintf "%G%s%Gi" v.Real sg (abs v.Imaginary)

    match (abs z.Phase) % Math.PI  with
    | c when c > 1E-15 -> toString z |> Complex
    | _ -> Real z.Real
 
let toObject z = 
   match z with
   | Complex s -> s :> obj
   | Real f -> f :> obj