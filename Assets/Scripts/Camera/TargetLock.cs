using UnityEngine;

/// Centers the camera between the two transform and stays a certain distance behind target 1.
public class TargetLock : MonoBehaviour {

    [SerializeField] private float _minDistance = 2;
	[SerializeField] private float _maxDistance = 10;
    [SerializeField] private Transform _target1;
    [SerializeField] private Transform _target2;
	[SerializeField] private float _maxHOffset = 3;
	[SerializeField] private bool _lerp = false;
	[SerializeField] private float _camSpeed = 10;
	
	private float _distance;
	private float _hOffset;
	private float _distHOffset;
	private Vector3 _wantedPosition;
	private Vector3 _staticOffset;
	private Vector3 _lastT1Pos;

	public static Transform[] Targets { get; private set; }
	
	void Start () 
	{
		if(!_target1 || !_target2) Destroy(this); // terminate this script if there are not two targets.
		_staticOffset = new Vector3(0, 2f, 0);
		_distance = 4;
		_hOffset = -1f;
		_distHOffset = _hOffset;
		_wantedPosition = _target1.position;
		_lastT1Pos = _target1.position;

		Targets = new Transform[]
		{
			_target1, _target2
		};
	}
	
	void LateUpdate ()
	{
		var lookAtPoint = _target1.position + _target2.position;
		lookAtPoint.x /= 2;
		lookAtPoint.y /= 2;
		lookAtPoint.z /= 2;

		var vec = (_target1.position - _lastT1Pos);
		var mag = vec.magnitude;
		
		var dist = Vector3.Project(vec, transform.right);
		var sign = Mathf.Sign(transform.InverseTransformVector(dist).x);
		_hOffset += dist.magnitude * sign;
		
		var targetvector = _target2.position - _target1.position;
		targetvector.y = 0;
		var targetvectornormal = targetvector.normalized;
		var cross = Vector3.Cross(targetvectornormal, Vector3.up);
		cross.y = 0;
		
		SetDist(targetvectornormal);
		
		if (Mathf.Abs(_hOffset) > _maxHOffset && mag > 0.001f)
		{
			_hOffset = _maxHOffset * Mathf.Sign(_hOffset);
			_distHOffset = _hOffset;
		}

		SetHorizontalPos(cross, _hOffset);
		
		transform.position = _lerp ? 
			Vector3.Lerp(transform.position, _wantedPosition, 
				Time.deltaTime * _camSpeed) : _wantedPosition;
		
		transform.LookAt(lookAtPoint);
		_lastT1Pos = _target1.position;
	}

	private void SetDist(Vector3 targetvectornormal)
	{
		_wantedPosition = _target1.position + (-targetvectornormal * _distance) + _staticOffset;
	}

	private void SetHorizontalPos(Vector3 cross, float h)
	{
		_wantedPosition += cross.normalized * h;
	}
	
}
