using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToucjPad : MonoBehaviour
{

    //_touchPad ������Ʈ�� �����մϴ�.
    private RectTransform _touchPad;

    //��ġ �Է� �߿� ���� ��Ʈ�ѷ��� ���� �ȿ� �ִ� �Է��� �����ϱ� ���� ���̵� �Դϴ�.
    private int _touchId = -1;

    //�Է��� ���۵Ǵ� ��ǥ�Դϴ�.
    private Vector3 _startPos = Vector3.zero;

    //���� ��Ʈ�ѷ��� ������ �����̴� �������Դϴ�.
    public float _dragRadius = 60f;

    //�÷��̾��� �������� �����ϴ� PlayerMovement ��ũ��Ʈ�� ����
    //����Ű�� ����Ǹ� ĳ���Ϳ��� ��ȣ�� ������ �ϱ� ����
    public PlayerMovement _player;

    //��ư�� ���ȴ��� üũ�ϴ� bool ����
    private bool _buttonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        //��ġ�е��� RectTransform ������Ʈ�� �����´�.
        _touchPad = GetComponent<RectTransform>();
        //��ġ �е��� ��ǥ�� �����´�. �������� ���ذ��� �ȴ�.
        _startPos = _touchPad.position;
    }

    public void ButtonDown()
    {
        //��ư�� ���ȴ��� Ȯ���� �����ϴ�.
        _buttonPressed = true;
    }

    public void ButtonUp()
    {
        _buttonPressed = false;
        //��ư�� �������� �� ��ġ�е�� ��ǥ�� ���� �������� ���ͽ�ŵ�ϴ�.
        HandleInput(_startPos);
    }

    private void FixedUpdate()
    {
        //����Ͽ����� ��ġ�е� ������� ���� ��ġ �Է��� �޾� ó��
        HandleTouchInput();

        //������� �ƴ� PC�� ����Ƽ ������ �󿡼� �۵��� ���� ��ġ �Է��� �ƴ� ���콺�� �Է� ����
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER

        HandleInput(Input.mousePosition);

#endif

    }


    void HandleTouchInput()
    {
        //��ġ���̵� �ű�� ���� ��ȣ
        int i = 0;
        //��ġ �Է��� �ѹ��� ���� ���� ���� �� �ִ�. ��ġ�� �ϳ� �̻� �ԷµǸ� ����ǵ�����
        if (Input.touchCount > 0)
        {
            //������ ��ġ �Է��� �ϳ��� ��ȸ�Ѵ�.
            foreach (Touch touch in Input.touches)
            {
                //��ġ���̵� �ű�� ���� ��ȣ�� 1 ���� ��Ų��
                i++;
                //���� ��ġ �Է��� x,y��ǥ�� ���մϴ�.
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);

                //��ġ �Է��� ��� ���۵Ǿ��ٸ�( TouchPhase.Began�̸�)
                if (touch.phase == TouchPhase.Began)
                {
                    //��ġ�� ��ǥ�� ���� ����Ű ���� ���� �ִٸ� 
                    if (touch.position.x <= (_startPos.x + _dragRadius))
                    {
                        //�� ��ġ ���̵� �������� ���� ��Ʈ�ѷ��� �����ϵ��� �Ѵ�
                        _touchId = i;
                    }
                }

                //��ġ �Է��� �������ٰų�, ������ �ִ� ��Ȳ�̶��,
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    //��ġ ���̵�� ������ ��쿡��
                    if (_touchId == i)
                    {
                        //��ǥ �Է��� �޾Ƶ��δ�.
                        HandleInput(touchPos);
                    }
                }

                //��ġ �Է��� �����µ�, 
                if (touch.phase == TouchPhase.Ended)
                {
                    //�Է¹ް��� �ߴ� ��ġ ���̵���
                    if (_touchId == i)
                    {
                        //��ġ ���̵� ����
                        _touchId = -1;
                    }
                }
            }
        }
    }

    void HandleInput(Vector3 input)
    {
        //��ư�� ������ ��Ȳ�̶��,
        if (_buttonPressed)
        {
            //���� ��Ʈ�ѷ��� ���� ��ǥ�κ��� �Է¹��� ��ǥ�� �󸶳� ������ �ִ��� ���Ѵ�.
            Vector3 diffVector = (input - _startPos);
            //�̷� ������ ���� ��ǥ�� �Ÿ��� ���մϴ�. ���� �ִ�ġ���� ũ�ٸ�,
            if (diffVector.sqrMagnitude > _dragRadius * _dragRadius)
            {
                //���⺤���� �Ÿ��� 1�� ����ϴ�.(��������, ����ȭ)
                diffVector.Normalize();
                //�׸��� ���� ��Ʈ�ѷ��� �ִ�ġ��ũ�� �����̰� ��.
                _touchPad.position = _startPos + diffVector * _dragRadius;
            }
            else  //�Է� ������ ���� ��ǥ�� �ִ�ġ���� ���� �ʴٸ�
            {

                //���� �Է� ��ǥ�� ����Ű�� �̵� ��Ŵ.
                _touchPad.position = input;
            }
        }
        else
        {
            //��ư���� ���� ��������, ����Ű�� ���� ��ġ�� �ǵ��� ����.
            _touchPad.position = _startPos;
        }
        //����Ű�� ���� ������ ���̸� ����
        Vector3 diff = _touchPad.position - _startPos;

        //����Ű�� ������ ������ ä��, �Ÿ��� ������ ���⸸ ���մϴ�.
        Vector2 normDiff = new Vector3(diff.x / _dragRadius, diff.y / _dragRadius);

        if (_player != null)
        {
            //�÷��̾ ����Ǿ� ������, �÷��̾�� ����� ��ǥ�� �������ش�.
            _player.OnStickChanged(normDiff);
        }



    }

    // Update is called once per frame
    void Update()
    {

    }
}

