module NumericsWSF

open ExcelDna.Integration
open MathNet.Numerics.Interpolation

[<ExcelFunction(Description="My first .NET function")>]
let lininterp (x1:double[]) (y1:double[]) (x2:double[]) =
    let interp = LinearSpline.Interpolate(x1, y1)
    Array.map interp.Interpolate x2
