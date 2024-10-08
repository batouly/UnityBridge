using std_msgs;
using std_msgs.msg;
using geometry_msgs.msg;
using sensor_msgs.msg;

namespace vision_msgs
{
    namespace msg
    {
        public class BoundingBox3D : IRosMsg
        {
            string IRosMsg.GetRosType()
            {
                return "vision_msgs.msg.BoundingBox3D";
            }

            /// <summary>
            /// The 3D position and orientation of the bounding box center.
            /// </summary>
            public Pose center;

            /// <summary>
            /// The size of the bounding box, in meters, surrounding the object's center pose.
            /// </summary>
            public Vector3 size;
        }

        public class BoundingBox3DArray : IRosMsg, IStamped
        {
            string IRosMsg.GetRosType()
            {
                return "vision_msgs.msg.BoundingBox3DArray";
            }

            public Header header { get; set; } = new Header();

            public BoundingBox3D[] boxes;

        }

        public class ObjectHypothesis : IRosMsg
        {
            string IRosMsg.GetRosType()
            {
                return "vision_msgs.msg.ObjectHypothesis";
            }

#if ROS_V2
            /// <summary>
            /// The unique string ID of the object class.
            /// </summary>
            public string class_id;
#else
            /// <summary>
            /// The unique numeric ID of object detected. 
            /// </summary>
            public long id;
#endif

            /// <summary>
            /// The probability or confidence value of the detected object.
            /// </summary>
            public double score;
        }


        public class ObjectHypothesisWithPose : IRosMsg
        {
            string IRosMsg.GetRosType()
            {
                return "vision_msgs.msg.ObjectHypothesisWithPose";
            }
#if ROS_V2
            /// <summary>
            /// The object hypothesis (ID and score).
            /// </summary>
            public ObjectHypothesis hypothesis;
#else
            /// <summary>
            /// The unique numeric ID of object detected.
            /// </summary>
            public long id;

            /// <summary>
            /// The probability or confidence value of the detected object. By convention,
            /// this value should lie in the range [0-1].
            /// </summary>
            public double score;
#endif

            /// <summary>
            /// The 6D pose of the object hypothesis. This pose should be
            /// defined as the pose of some fixed reference point on the object,
            /// such as the geometric center of the bounding box or the center of mass.
            /// </summary>
            public PoseWithCovariance pose;
        }


        public class Detection3D : IRosMsg, IStamped
        {
            string IRosMsg.GetRosType()
            {
                return "vision_msgs.msg.Detection3D";
            }

            public Header header { get; set; } = new Header();

            /// <summary>
            /// Class probabilities. Does not have to include hypotheses for all possible
            /// object ids, the scores for any ids not listed are assumed to be 0.
            /// </summary>
            public ObjectHypothesisWithPose[] results;

            /// <summary>
            /// 3D bounding box surrounding the object.
            /// </summary>
            public BoundingBox3D bbox;

#if ROS_V2
            /// <summary>
            /// ID used for consistency across multiple detection messages. Detections
            /// of the same object in different detection messages should have the same id.
            /// This field may be empty.
            /// </summary>
            public string id;
#else

            /// <summary>
            ///The 3D data that generated these results (i.e. region proposal cropped out of
            ///the image). This information is not required for all detectors, so it may
            ///be empty..
            /// </summary>
            public PointCloud2 source_cloud;
#endif

        }

        public class Detection3DArray : IRosMsg, IStamped
        {
            string IRosMsg.GetRosType()
            {
                return "vision_msgs.msg.Detection3DArray";
            }

            public Header header { get; set; } = new Header();

            /// <summary>
            /// A list of the detected proposals. A multi-proposal detector might generate
            /// this list with many candidate detections generated from a single input.
            /// </summary>
            public Detection3D[] detections;
        }
    }

}
