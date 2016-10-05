using UnityEngine;
using System.Collections;

public class SimpleMeshGenerator : MonoBehaviour {

	public int N;
	public float width;
	public float longWidth;
	public float timeFactor;

	Mesh _mesh;
	Vector3[] _vs;
	int[] _inds;

	void Start () {

		_vs = new Vector3[2*N];
		_inds = new int[6*(N-1)];

		int k=0;
		for(int i=0;i<N-1;i++){

			_inds[0+k] = 2*i;
			_inds[1+k] = 2*i+1;
			_inds[2+k] = 2*(i+1)+1;
			_inds[3+k] = 2*(i+1)+1;
			_inds[4+k] = 2*(i+1);
			_inds[5+k] = 2*i;

			k+=6;
		}


		_mesh = new Mesh();

		GenerateMesh();

		_mesh.SetIndices(_inds,MeshTopology.Triangles,0);

		GetComponent<MeshFilter>().mesh = _mesh;

	}

	void GenerateMesh(){
		for(int i=0;i<N;i=i+1){

			Vector3 c = Curve(longWidth*1f*i/N);
			Vector3 corto = DerivateCurve(longWidth*1f*i/N,longWidth*1f/N);
			corto = OrtoDerivateCurve(corto);

			_vs[2*i] = c - width*corto.normalized;
			_vs[2*i+1] = c + width*corto.normalized;

		}

		_mesh.vertices = _vs;


	}

	void Update(){
		GenerateMesh();
	}

	public Vector3 Curve(float t){

		t = t+timeFactor*Time.time;

		float xt = Mathf.Cos(5f*t)+3f*Mathf.Sin(4f*t);
		float yt = 3f*Mathf.Sin(3f*t)+2f*Mathf.Sin(5f*t);
		return new Vector3(xt,yt,0f);

	}

	public Vector3 DerivateCurve(float t,float dt){

		return (Curve(t+dt)-Curve(t-dt))/(2f*dt);

	}

	public Vector3 OrtoDerivateCurve(Vector3 v){

		Vector3 vorto = v;
		vorto.y = v.x;
		vorto.x = -v.y;

		return vorto;

	}
	

}
