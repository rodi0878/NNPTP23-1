namespace NNPTPZ1.Mathematics
{
    public class PolygonPair
    {
        public Polygon Polygon { get; }
        public Polygon PolygonDerived { get; }
        
        public PolygonPair(Polygon polygon, Polygon polygonDerived)
        {
            Polygon = polygon;
            PolygonDerived = polygonDerived;
        }
    }
}