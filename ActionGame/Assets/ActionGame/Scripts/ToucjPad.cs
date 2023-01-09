using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToucjPad : MonoBehaviour
{

    //_touchPad 오브젝트를 연결합니다.
    private RectTransform _touchPad;

    //터치 입력 중에 방향 컨트롤러의 영역 안에 있는 입력을 구분하기 위한 아이디 입니다.
    private int _touchId = -1;

    //입력이 시작되는 좌표입니다.
    private Vector3 _startPos = Vector3.zero;

    //방향 컨트롤러가 원으로 움직이는 반지름입니다.
    public float _dragRadius = 60f;

    //플레이어의 움직임을 관리하는 PlayerMovement 스크립트와 연결
    //방향키가 변경되면 캐릭터에게 신호를 보내야 하기 때문
    public PlayerMovement _player;

    //버튼이 눌렸는지 체크하는 bool 변수
    private bool _buttonPressed = false;

    // Start is called before the first frame update
    void Start()
    {
        //터치패드의 RectTransform 오브젝트를 가져온다.
        _touchPad = GetComponent<RectTransform>();
        //터치 패드의 좌표를 가져온다. 움직임의 기준값이 된다.
        _startPos = _touchPad.position;
    }

    public void ButtonDown()
    {
        //버튼이 눌렸는지 확인해 놓습니다.
        _buttonPressed = true;
    }

    public void ButtonUp()
    {
        _buttonPressed = false;
        //버튼이 떼어졌을 때 터치패드와 좌표를 원래 지점으로 복귀시킵니다.
        HandleInput(_startPos);
    }

    private void FixedUpdate()
    {
        //모바일에서는 터치패드 방식으로 여러 터치 입력을 받아 처리
        HandleTouchInput();

        //모바일이 아닌 PC나 유니티 에디터 상에서 작동할 때는 터치 입력이 아닌 마우스로 입력 받음
#if UNITY_EDITOR || UNITY_STANDALONE_OSX || UNITY_STANDALONE_WIN || UNITY_WEBPLAYER

        HandleInput(Input.mousePosition);

#endif

    }


    void HandleTouchInput()
    {
        //터치아이디를 매기기 위한 번호
        int i = 0;
        //터치 입력은 한번에 여러 개가 들어올 수 있다. 터치가 하나 이상 입력되면 실행되도록함
        if (Input.touchCount > 0)
        {
            //각각의 터치 입력을 하나씩 조회한다.
            foreach (Touch touch in Input.touches)
            {
                //터치아이디를 매기기 위한 번호를 1 증가 시킨다
                i++;
                //현재 터치 입력의 x,y좌표를 구합니다.
                Vector3 touchPos = new Vector3(touch.position.x, touch.position.y);

                //터치 입력이 방금 시작되었다면( TouchPhase.Began이면)
                if (touch.phase == TouchPhase.Began)
                {
                    //터치의 좌표가 현재 방향키 범위 내에 있다면 
                    if (touch.position.x <= (_startPos.x + _dragRadius))
                    {
                        //이 터치 아이디를 기준으로 방향 컨트롤러를 조작하도록 한다
                        _touchId = i;
                    }
                }

                //터치 입력이 움직였다거나, 가만히 있는 상황이라면,
                if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
                {
                    //터치 아이디로 지정된 경우에만
                    if (_touchId == i)
                    {
                        //좌표 입력을 받아들인다.
                        HandleInput(touchPos);
                    }
                }

                //터치 입력이 끝났는데, 
                if (touch.phase == TouchPhase.Ended)
                {
                    //입력받고자 했던 터치 아이디라면
                    if (_touchId == i)
                    {
                        //터치 아이디를 해제
                        _touchId = -1;
                    }
                }
            }
        }
    }

    void HandleInput(Vector3 input)
    {
        //버튼이 눌러진 상황이라면,
        if (_buttonPressed)
        {
            //방향 컨트롤러의 기준 좌표로부터 입력받은 좌표가 얼마나 떨어져 있는지 구한다.
            Vector3 diffVector = (input - _startPos);
            //이력 지점과 기준 좌표의 거리를 비교합니다. 만약 최대치보다 크다면,
            if (diffVector.sqrMagnitude > _dragRadius * _dragRadius)
            {
                //방향벡터의 거리를 1로 만듭니다.(단위벡터, 정규화)
                diffVector.Normalize();
                //그리고 방향 컨트롤러는 최대치만크만 움직이게 함.
                _touchPad.position = _startPos + diffVector * _dragRadius;
            }
            else  //입력 지점과 기준 좌표가 최대치보다 그지 않다면
            {

                //현재 입력 좌표에 방향키를 이동 시킴.
                _touchPad.position = input;
            }
        }
        else
        {
            //버튼에서 손이 떼어지면, 방향키를 원래 위치로 되돌려 놓음.
            _touchPad.position = _startPos;
        }
        //방향키와 기준 지점의 차이를 구함
        Vector3 diff = _touchPad.position - _startPos;

        //방향키의 방향을 유지한 채로, 거리를 나누어 방향만 구합니다.
        Vector2 normDiff = new Vector3(diff.x / _dragRadius, diff.y / _dragRadius);

        if (_player != null)
        {
            //플레이어가 연결되어 있으면, 플레이어에게 변경된 좌표를 전달해준다.
            _player.OnStickChanged(normDiff);
        }



    }

    // Update is called once per frame
    void Update()
    {

    }
}

