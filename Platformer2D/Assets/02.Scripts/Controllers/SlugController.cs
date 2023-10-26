using Platformer.FSM;
using UnityEngine;

namespace Platformer.Controllers
{
    public class SlugController : EnemyController
    {
        public bool isGroundForwardExist
        {
            get
            {
                Vector2 detectGroundForwardStartPos
                    = rigidbody.position + new Vector2(_detectGroundForwardOffset.x * direction, _detectGroundForwardOffset.y);

                RaycastHit2D hit = Physics2D.Raycast(detectGroundForwardStartPos, Vector2.down, _detectGroundForwardDistance, _groundForwardMask);

                _groundForward = hit.collider;

                return _groundForward;
            }
        }

        [SerializeField] private Collider2D _groundForward;
        [SerializeField] private Vector2 _detectGroundForwardOffset;
        [SerializeField] private float _detectGroundForwardDistance;
        [SerializeField] LayerMask _groundForwardMask;

        protected override void Start()
        {
            base.Start();

            machine = new EnemyMachine(this);
            var machineData = StateMachineDataSheet.GetSlugData(machine);
            machine.Init(machineData);
            onHpDepleted += (amount) => machine.ChangeState(CharacterStateID.Hurt);
            onHpMin += () => machine.ChangeState(CharacterStateID.Die);
        }

        protected override void FixedUpdate()
        {
            if (isGroundForwardExist == false)
                return;

            base.FixedUpdate();
        }


        protected override void OnDrawGizmosSelected()
        {
            base.OnDrawGizmosSelected();
            DrawDetectGroundForwardGizmos();
        }

        void DrawDetectGroundForwardGizmos()
        {
            Gizmos.color = Color.blue;

            Vector2 detectGroundForwardStartPos
                = rigidbody.position + new Vector2(_detectGroundForwardOffset.x * direction, _detectGroundForwardOffset.y);

            Gizmos.DrawLine(detectGroundForwardStartPos, detectGroundForwardStartPos + Vector2.down * _detectGroundForwardDistance);
        }
    }
}