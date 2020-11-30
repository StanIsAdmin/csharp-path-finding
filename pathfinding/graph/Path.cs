using System;
using System.Collections.Generic;

namespace graph {
    public class Path : IComparable<Path> {
        public Path parent;
        public Vertex vertex;
        public int length { get; }
        private float gCost;
        private float hCost;
        public float cost {
            get {
                return this.gCost + this.hCost;
            }
        }

        /// Create a Path with only the source
        public Path(Vertex source) {
            this.parent = null;
            this.gCost = 0f;
            this.hCost = 0f;
            this.length = 0;
            this.vertex = source;
        }

        /// Build a NodePath with a parent
        public Path(Vertex vertex, Path parent, float hCost, float edgeCost) {
            this.vertex = vertex;
            this.parent = parent;
            this.gCost = parent.gCost + edgeCost;
            this.hCost = hCost;
            this.length = parent.length + 1;
        }

        public List<Edge> GetEdges() {
            return this.vertex.GetEdges();
        }

        public int CompareTo(Path other) {
            if (this.cost < other.cost) {
                return -1;
            } else if (this.cost > other.cost) {
                return 1;
            } else { // If both paths have the same cost, compare the heuristics
                if (this.hCost > other.hCost) {
                    return 1;
                } else if (this.hCost < other.hCost) {
                    return -1;
                }
            }
            return 0;
        }

        public override int GetHashCode() {
            return this.vertex.GetHashCode();
        }

        public List<Vertex> ToList() {
            List<Vertex> res = new List<Vertex>();
            Path current = this;
            while (current != null) {
                res.Insert(0, current.vertex);
                current = current.parent;
            }
            return res;
        }

        public override bool Equals(object obj) {
            if (obj == null || obj.GetType() != this.GetType()) {
                return false;
            }
            if (this == obj) {
                return true;
            }
            Path other = (Path)obj;
            return other.vertex.Equals(this.vertex);
        }

        public override string ToString() {
            // if (this.parent == null) {
            //     return this.vertex.ToString();
            // }
            // return this.parent.ToString() + "->" + this.vertex.ToString();
            return this.vertex.ToString();
        }


    }

}

