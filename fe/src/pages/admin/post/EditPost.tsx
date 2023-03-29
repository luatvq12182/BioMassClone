import { useParams } from "react-router-dom";

const EditPost = () => {
    const { id } = useParams();

    console.log('Id param from url: ', id);

    return <div>EditPost</div>;
};

export default EditPost;
