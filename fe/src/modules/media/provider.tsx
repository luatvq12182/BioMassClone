import { ReactNode } from "react";
import { IMedia } from "./models";
import useMedia from "./queries";

type Props = {
    render: (media: IMedia[], isLoading?: boolean) => ReactNode;
};

const MediaProvider = ({ render }: Props) => {
    const { data: media, isFetching } = useMedia();

    return <>{render(media?.data ? media.data : [], isFetching)}</>;
};

export { MediaProvider };
