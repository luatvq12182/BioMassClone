import { ProgressSpinner } from "primereact/progressspinner";

type Props = {
    loading: boolean;
};

const Loading = ({ loading }: Props) => {
    if (!loading) return <div />;

    return (
        <div className='flex items-center content-center fixed top-0 left-0 right-0 bottom-0 z-[9999] backdrop-blur-md'>
            <ProgressSpinner
                style={{ width: "100px", height: "100px" }}
                strokeWidth='4'
                // fill='var(--surface-ground)'
                animationDuration='.6s'
            />
        </div>
    );
};

export default Loading;
