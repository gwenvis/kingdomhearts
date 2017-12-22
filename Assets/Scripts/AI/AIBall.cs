using UnityEngine;

// Created by Antonio Bottelier
public class AIBall : MonoBehaviour
{
    private float speed = 15;
    private float height = 2;
    private float x, a, c, o;
    private float distance, heightdifference;
    
    private Vector3 origin, direction;
    private static AudioClip hit;
    private static AudioClip whoosh;
    private bool playedWhooshSound;

    void Start() {
        Destroy(gameObject, 5); // door Timo
        if (!hit)
            hit = Resources.Load<AudioClip>("ball_hit");
        if (!whoosh)
            whoosh = Resources.Load<AudioClip>("whoosh");
    }

    // Maybe a minimum distance is needed? maybe.
    public void SetValues(Vector3 origin, Vector3 direction, float distance, float heightdifference)
    {
        this.origin = origin;
        this.direction = direction;
        this.direction.y = 0;
        this.direction.Normalize();
        this.distance = distance;
        //Debug.Log(distance);
        if (this.distance < 6) this.distance = 6;
        this.heightdifference = heightdifference;
        
        x = 0;
        c = this.distance / 2;
        a = height / (c * c);
    }

    private void Update()
    {
        // y = -(ax^2) + b
        float y = -(a*(x-c)*(x-c)) + height;
        o = x / distance * heightdifference;

        var pos = direction * x;
        pos.y = y + o;
        transform.position = origin + pos;

        if (x > distance / 2 && !playedWhooshSound)
        {
            SoundManager.PlaySoundAt(transform.position, whoosh);
            playedWhooshSound = true;
        }
        
        x += Time.deltaTime * speed;

        RaycastHit info;
        if (Physics.Raycast(transform.position, Vector3.up, out info))
        {
            if (info.collider.CompareTag("Ground"))
            {
                SoundManager.PlaySoundAt(transform.position, hit);
                Destroy(gameObject);
            }
        }
    }

    // Functie door Timo
    private void OnTriggerEnter(Collider other) {
        if (other.gameObject.CompareTag("Player"))
        {
            GameObject hit = UnityEngine.Resources.Load<GameObject>("HitBall");
            GameObject particle = Instantiate(hit);
            particle.transform.position = transform.position;
            Destroy(particle, 2);
            Destroy(gameObject);
        }
    }
}