using UnityEngine;
namespace LPE.Math {
    public static partial class Geometry {
      public static Vector2 ClosestOnTriangle(Vector2 p, Vector2 a, Vector2 b, Vector2 c) {
            Vector2 ab = b - a;
            Vector2 ac = c - a;
            Vector2 ap = p - a;

            float d1 = Vector2.Dot(ab, ap);
            float d2 = Vector2.Dot(ac, ap);
            if (d1 <= 0 && d2 <= 0) return a; //#1

            Vector2 bp = p - b;
            float d3 = Vector2.Dot(ab, bp);
            float d4 = Vector2.Dot(ac, bp);
            if (d3 >= 0 && d4 <= d3) return b; //#2

            Vector2 cp = p - c;
            float d5 = Vector2.Dot(ab, cp);
            float d6 = Vector2.Dot(ac, cp);
            if (d6 >= 0 && d5 <= d6) return c; //#3

            float vc = d1 * d4 - d3 * d2;
            if (vc <= 0 && d1 >= 0 && d3 <= 0) {
                float v = d1 / (d1 - d3);
                return a + v * ab; //#4
            }

            float vb = d5 * d2 - d1 * d6;
            if (vb <= 0 && d2 >= 0 && d6 <= 0) {
                float v = d2 / (d2 - d6);
                return a + v * ac; //#5
            }

            float va = d3 * d6 - d5 * d4;
            if (va <= 0 && (d4 - d3) >= 0 && (d5 - d6) >= 0) {
               float v = (d4 - d3) / ((d4 - d3) + (d5 - d6));
                return b + v * (c - b); //#6
            }

            float denom = 1 / (va + vb + vc);
            float v2 = vb * denom;
            float w = vc * denom;
            return a + v2 * ab + w * ac; //#0
        }
        public static bool InTriangle(Vector2 pt, Vector2 t1, Vector2 t2, Vector2 t3) {
            const float EPS = .00000001f;
            float d1, d2, d3;
            bool has_neg, has_pos;

            d1 = sign(pt, t1, t2);
            d2 = sign(pt, t2, t3);
            d3 = sign(pt, t3, t1);

            has_neg = (d1 < -EPS) || (d2 < -EPS) || (d3 < -EPS);
            has_pos = (d1 > EPS) || (d2 > EPS) || (d3 > EPS);

            return !(has_neg && has_pos);

            float sign(Vector2 p1, Vector2 p2, Vector2 p3) {
                return (p1.x - p3.x) * (p2.y - p3.y) - (p2.x - p3.x) * (p1.y - p3.y);
            }

        }
        public static bool InCircumcircle(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3) {
            if (IsClockwise(v1, v2, v3)) {
                var temp = v1;
                v1 = v2;
                v2 = temp;
            }

            float a = v1.x - pt.x;
            float b = v1.y - pt.y;
            float c = a * a + b * b;
            float d = v2.x - pt.x;
            float e = v2.y - pt.y;
            float f = d * d + e * e;
            float g = v3.x - pt.x;
            float h = v3.y - pt.y;
            float i = g * g + h * h;
            /*
             * a b c 
             * d e f
             * g h i
             */
            return a * (e * i - f * h) - b * (d * i - f * g) + c * (d * h - e * g) > 0;

        }
        public static double InCircumcircleF(Vector2 pt, Vector2 v1, Vector2 v2, Vector2 v3) {
            if (IsClockwise(v1, v2, v3)) {
                var temp = v1;
                v1 = v2;
                v2 = temp;
            }

            double a = v1.x - pt.x;
            double b = v1.y - pt.y;
            double c = a * a + b * b;
            double d = v2.x - pt.x;
            double e = v2.y - pt.y;
            double f = d * d + e * e;
            double g = v3.x - pt.x;
            double h = v3.y - pt.y;
            double i = g * g + h * h;
            /*
             * a b c 
             * d e f
             * g h i
             */
            return a * (e * i - f * h) - b * (d * i - f * g) + c * (d * h - e * g);

        }

        public static bool IsClockwise(Vector2 v1, Vector2 v2, Vector2 v3) {

            return v1.x * v2.y + v3.x * v1.y + v2.x * v3.y <
                   v3.x * v2.y + v1.x * v3.y + v2.x * v1.y;
        }

        public static float TriangleAltitudeSqr(Vector2 h, Vector2 a, Vector2 b) {
            float area = (a.x * b.y - a.y * b.x + b.x * h.y - b.y * h.x + a.y * h.x - a.x * h.y);
            float areaSqr = area * area;
            float baseSqr = (a - b).sqrMagnitude;
            return areaSqr / baseSqr;
        }
        public static float TriangleArea(Vector2 a, Vector2 b, Vector2 c) {
            return 0.5f * Mathf.Abs(a.x * b.y - a.y * b.x + b.x * c.y - b.y * c.x + a.y * c.x - a.x * c.y);
        }
    }
}