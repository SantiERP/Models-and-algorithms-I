using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SterringAgent : MonoBehaviour
{
    protected Vector3 _velocity;
    [SerializeField] float _viewRadiusSeparation;
    [SerializeField] float _viewRadiusAlignmentCohesion;
    [SerializeField] float _maxSpeed;
    [SerializeField] float _maxForce;

    protected Vector3 Separation(List<SterringAgent> enemy)
    {
        Vector3 desired = Vector3.zero;
        foreach (var item in enemy)
        {
            if (item == this) continue;
            Vector3 dist = item.transform.position - transform.position;
            if (dist.sqrMagnitude > _viewRadiusSeparation * _viewRadiusSeparation) continue;
            desired += dist;
        }
        if (desired == Vector3.zero) return desired;
        desired *= -1;
        return CalculateSteering(desired.normalized * _maxSpeed);

    }
    protected Vector3 CalculateSteering(Vector3 desired)
    {
        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);
        return steering;
    }

    protected Vector3 Cohesion(List<SterringAgent> agents)
    {
        //Promedio de posiciones de agentes de locales.
        Vector3 desired = Vector3.zero;
        int count = 0;
        foreach (var item in agents)
        {
            if (item == this) continue;
            if (Vector3.Distance(transform.position, item.transform.position) > _viewRadiusAlignmentCohesion)
                continue;

            desired += item.transform.position;
            count++;
        }
        if (count == 0) return Vector3.zero;

        //Promedio = Suma / Cant.
        desired /= count;
        return Seek(desired);
    }

    public Vector3 Seek(Vector3 targetPos)
    {
        return Seek(targetPos, _maxSpeed);
    }

    public Vector3 Seek(Vector3 targetPos, float speed)
    {
        Vector3 desired = targetPos - transform.position;
        desired.Normalize();
        desired *= speed;

        Vector3 steering = desired - _velocity;
        steering = Vector3.ClampMagnitude(steering, _maxForce * Time.deltaTime);

        return steering;
    }
    public void AddForce(Vector3 force)
    {
        _velocity = Vector3.ClampMagnitude(_velocity + force, _maxSpeed);
    }

}
