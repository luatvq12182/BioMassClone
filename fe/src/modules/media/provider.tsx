import { ReactNode } from "react";
import { IMedia } from "./models";
import useMedia from "./queries";

type Props = {
    isSlider: boolean;
    render: (media: IMedia[], isLoading?: boolean) => ReactNode;
};

const MediaProvider = ({ render, isSlider }: Props) => {
    const { data: media, isFetching } = useMedia();

    const data = media?.data ? media.data : [];

    return (
        <>
            {render(
                !isSlider
                    ? data
                    : data.filter((i) => {
                          return i.showOnSlider;
                      }),
                isFetching
            )}
        </>
    );
};

export { MediaProvider };
