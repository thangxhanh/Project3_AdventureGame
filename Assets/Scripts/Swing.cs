using UnityEngine;

public class Swing : MonoBehaviour
{
    [SerializeField][Range(0, 90)] float _swingAngle = 33;
    [SerializeField][Min(0.001f)] float _length = 1.4f;
    [SerializeField][Min(0.001f)] float _gravity = 6f;
    [SerializeField] float _azimuth = 0;
    [SerializeField] int _ropeSegments = 10;
    [SerializeField] float _ropeWidth = 0.1f;
    public GameObject spritePrefab; // Reference to the prefab of the Sprite Renderer
    public GameObject spriteRopePrefab; // Reference to the prefab of the Rope Sprite Renderer
    public float circleRadius = 0.1f; // Radius of the circle to visualize

    private SpriteRenderer _spriteRenderer; // Reference to the added Sprite Renderer
    private SpriteRenderer _spriteRopeRenderer; // Reference to the added Sprite Renderer
    private LineRenderer _lineRenderer; // Reference to the Line Renderer component
    private GameObject _ropeContainer; // GameObject to hold the rope sprites

    void Start()
    {
        
        // Instantiate the spritePrefab and assign it to the _spriteRenderer
        GameObject spriteObj = Instantiate(spritePrefab, transform.position, Quaternion.identity);
        GameObject spriteRopeObj = Instantiate(spriteRopePrefab, transform.position, Quaternion.identity);
        _spriteRenderer = spriteObj.GetComponent<SpriteRenderer>();
        _spriteRopeRenderer = spriteRopeObj.GetComponent<SpriteRenderer>();

        // Create a GameObject to hold the rope sprites
        _ropeContainer = new GameObject("RopeContainer");

        // Add and configure the Line Renderer component
        _lineRenderer = gameObject.AddComponent<LineRenderer>();
        _lineRenderer.positionCount = _ropeSegments;
        _lineRenderer.startWidth = _ropeWidth;
        _lineRenderer.endWidth = _ropeWidth;

        // Create and attach Sprite Renderers to the rope container
        for (int i = 0; i < _ropeSegments; i++)
        {
            GameObject ropeSegment = new GameObject("RopeSegment_" + i);
            ropeSegment.transform.parent = _ropeContainer.transform;

            SpriteRenderer ropeSegmentRenderer = ropeSegment.AddComponent<SpriteRenderer>();
            ropeSegmentRenderer.sprite = _spriteRopeRenderer.sprite;
            ropeSegmentRenderer.color = _spriteRopeRenderer.color;
        }
    }

    void Update()
    {
        float time = Time.time;
        float angle = -90f + (_swingAngle * Mathf.Deg2Rad * Mathf.Cos(Mathf.Sqrt(_gravity / _length) * time)) * Mathf.Rad2Deg;

        Vector3 p0 = transform.position;
        Vector3 p1 = p0 + Quaternion.Euler(0, _azimuth, angle) * new Vector3 { x = _length };


        // Set the position and rotation of the added Sprite Renderer
        _spriteRenderer.transform.position = p1;
        _spriteRenderer.transform.rotation = Quaternion.Euler(0, _azimuth, angle);

        // Update the positions of the Line Renderer to create a rope-like effect
        for (int i = 0; i < _ropeSegments; i++)
        {
            float t = i / (float)(_ropeSegments - 1);
            Vector3 segmentPos = Vector3.Lerp(p0, p1, t);
            _lineRenderer.SetPosition(i, segmentPos);

            // Set the position of each rope segment
            Transform ropeSegment = _ropeContainer.transform.GetChild(i);
            ropeSegment.position = segmentPos;
        }
    }
}
